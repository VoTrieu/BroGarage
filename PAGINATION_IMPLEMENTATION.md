# BroGarage Pagination Implementation Guide

## Overview
Pagination has been standardized and enhanced across the BroGarage API and Web frontend. The implementation now includes:
- **Reusable pagination extensions** to eliminate code duplication
- **Sorting support** with dynamic orderBy and sortDirection parameters
- **Consistent pagination response structure** across all endpoints
- **Frontend service updates** to support sorting

---

## Backend Implementation

### 1. Core Models

#### `PaginationModel<T>` (Updated)
Location: `API/Shared/Models/PaginationModel.cs`

```csharp
public class PaginationModel<T>
{
    public int TotalRow { get; set; }
    public int TotalPage { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public T? Data { get; set; }
    public string? OrderBy { get; set; }           // NEW: Current sort field
    public string SortDirection { get; set; }      // NEW: Sort direction (asc/desc)
}
```

#### `PaginationRequestModel` (New)
Location: `API/Shared/RequestModels/Common/PaginationRequestModel.cs`

Base class for standardizing pagination request parameters:

```csharp
public class PaginationRequestModel
{
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 1;
    public string? OrderBy { get; set; }
    public string SortDirection { get; set; } = "desc";
    public string? Keyword { get; set; }
}
```

### 2. Extension Methods

Location: `API/Shared/Extensions/PaginationExtensions.cs` (New)

#### `Paginate<T>()` - DRY Skip/Take
```csharp
public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageSize, int pageIndex)
{
    int skipItem = pageSize * (pageIndex - 1);
    return query.Skip(skipItem).Take(pageSize);
}
```

#### `ApplySort<T>()` - Dynamic Sorting
```csharp
public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string? orderBy, string sortDirection = "desc")
    where T : class
{
    // Dynamically applies ORDER BY based on property name and direction
    // Example: ApplySort("Name", "asc") → ORDER BY Name ASC
}
```

#### `GetPaginationMetadata()` - Calculate Pagination Info
```csharp
public static (int TotalPage, int SkipItem) GetPaginationMetadata(int totalRow, int pageSize, int pageIndex)
{
    int skipItem = pageSize * (pageIndex - 1);
    int totalPage = (int)Math.Ceiling((decimal)totalRow / pageSize);
    return (totalPage, skipItem);
}
```

### 3. Refactored Controllers

All 7 paginated controllers have been updated to use the new extensions:

| Controller | Endpoint | Sortable Fields | Status |
|-----------|----------|-----------------|--------|
| **CarController** | `GET /car/get-pagination` | CarId, LicensePlate, YearOfManufacture | ✅ |
| **CarTypeController** | `GET /car-type/get-pagination` | TypeId, TypeName | ✅ |
| **ManufacturerController** | `GET /manufacturer/get-pagination` | ManufacturerId, ManufacturerName | ✅ |
| **CustomerController** | `GET /customer/get-pagination` | CustomerId, FullName, PhoneNumber | ✅ |
| **ProductController** | `GET /product/get-pagination` | ProductId, ProductCode, ProductName | ✅ |
| **TemplateController** | `GET /template/get-pagination` | TemplateId, YearOfManufactureFrom | ✅ |
| **OrderController** | `GET /order/get-pagination` | OrderId, OrderDate, DateIn | ✅ |

#### Example: Before and After

**Before (Duplicated Code):**
```csharp
int skipItem = pageSize * (pageIndex - 1);
int totalRow = query != null ? await query.CountAsync() : 0;
int totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRow) / pageSize));

var data = await query
    .Skip(skipItem)
    .Take(pageSize)
    .Select(...)
    .ToArrayAsync();
```

**After (With Extensions):**
```csharp
int totalRow = query != null ? await query.CountAsync() : 0;
var (totalPage, skipItem) = PaginationExtensions.GetPaginationMetadata(totalRow, pageSize, pageIndex);

var data = await query
    .Paginate(pageSize, pageIndex)
    .Select(...)
    .ToArrayAsync();
```

### 4. API Request/Response Examples

#### Request with Sorting
```
GET /car/get-pagination?pageSize=10&pageIndex=1&keyword=BMW&orderBy=LicensePlate&sortDirection=asc
```

**Parameters:**
- `pageSize` - Items per page (default: 10)
- `pageIndex` - Page number, 1-based (default: 1)
- `keyword` - Search term (optional)
- `orderBy` - Field to sort by (e.g., "CarId", "LicensePlate") (default: entity ID field)
- `sortDirection` - "asc" or "desc" (default: "desc")

#### Response
```json
{
  "isSuccess": true,
  "message": null,
  "result": {
    "totalRow": 25,
    "totalPage": 3,
    "pageSize": 10,
    "pageIndex": 1,
    "orderBy": "LicensePlate",
    "sortDirection": "asc",
    "data": [
      { "carId": 1, "licensePlate": "29A-123456", ... },
      { "carId": 5, "licensePlate": "29A-234567", ... }
    ]
  },
  "code": 200
}
```

---

## Frontend Implementation

### 1. Updated Services

All service functions now accept sorting parameters:

#### `car-service.js`
```javascript
export const getCars = (
  pageSize = 10,
  pageIndex = 1,
  keyword = null,
  orderBy = "CarId",
  sortDirection = "desc",
  token = null
) => { ... }
```

#### `customer-service.js`, `spare-part-service.js`, `maintainance-cycle-service.js`
Updated similarly with `orderBy` and `sortDirection` parameters.

#### `repair-service.js` (Orders)
```javascript
export async function getRepairForms(
  pageSize = 10,
  pageIndex = 1,
  keyword = null,
  searchParameters = {},
  orderBy = "OrderId",
  sortDirection = "desc"
) { ... }
```

### 2. Usage in Components

```javascript
// With default sorting
const { data } = await getCars(10, 1, "BMW");

// With custom sorting
const { data } = await getCars(10, 1, "BMW", "LicensePlate", "asc");

// For orders with complex filters
const { data } = await getRepairForms(
  10, 
  1, 
  "Honda",
  { customerId: 5, statusId: 2 },
  "OrderDate",
  "desc"
);
```

### 3. AppDataTable Integration

The existing `AppDataTable.js` component uses PrimeReact's Paginator. The pagination response now includes sorting information which can be displayed in the UI.

---

## Migration Guide

### For New Endpoints

To add pagination to a new controller endpoint:

1. Add parameters to the action method:
```csharp
public async Task<ActionResult<ResponseModel<PaginationModel<TResModel[]>>>> GetPagination(
    int pageSize = 10,
    int pageIndex = 1,
    string? orderBy = "EntityId",
    string sortDirection = "desc"
)
```

2. Build your filter query:
```csharp
var query = db.Entities
    .Where(n => /* your filters */)
    .ApplySort(orderBy, sortDirection);
```

3. Get pagination metadata:
```csharp
int totalRow = await query.CountAsync();
var (totalPage, skipItem) = PaginationExtensions.GetPaginationMetadata(totalRow, pageSize, pageIndex);
```

4. Execute paginated query:
```csharp
var data = await query
    .Paginate(pageSize, pageIndex)
    .Select(...)
    .ToArrayAsync();
```

5. Return response:
```csharp
var pagination = new PaginationModel<TResModel[]>
{
    PageSize = pageSize,
    PageIndex = pageIndex,
    TotalPage = totalPage,
    TotalRow = totalRow,
    OrderBy = orderBy,
    SortDirection = sortDirection,
    Data = data
};

response.Result = pagination;
response.IsSuccess = true;
return Ok(response);
```

### For Existing Components

Update service calls to include sorting:

```javascript
// Old
const { data } = await getCars(10, 1, keyword);

// New (with sorting)
const { data } = await getCars(10, 1, keyword, "CarId", "desc");
```

---

## Benefits

✅ **Code Reusability** - Eliminated ~200+ lines of duplicated pagination logic  
✅ **Consistency** - All pagination endpoints follow the same pattern  
✅ **Sorting Support** - Dynamic sorting on any entity property  
✅ **Type Safety** - Maintained strong typing throughout  
✅ **Performance** - Efficient database queries with proper Skip/Take  
✅ **Maintainability** - Future changes in one place (extensions)  
✅ **Developer Experience** - Clear, intuitive API

---

## Testing Checklist

- [ ] Test pagination with different page sizes
- [ ] Test sorting in ascending and descending order
- [ ] Test sorting on various entity properties
- [ ] Test combinations of filters + sorting + pagination
- [ ] Verify OrderController complex filters still work
- [ ] Test frontend service calls with new parameters
- [ ] Run API and verify no regressions
- [ ] Check browser network tab for correct query parameters

---

## Files Modified

**Backend:**
- `API/Shared/Models/PaginationModel.cs` (Enhanced)
- `API/Shared/RequestModels/Common/PaginationRequestModel.cs` (New)
- `API/Shared/Extensions/PaginationExtensions.cs` (New)
- `API/Controllers/CarController.cs` (Refactored)
- `API/Controllers/CarTypeController.cs` (Refactored)
- `API/Controllers/ManufacturerController.cs` (Refactored)
- `API/Controllers/CustomerController.cs` (Refactored)
- `API/Controllers/ProductController.cs` (Refactored)
- `API/Controllers/TemplateController.cs` (Refactored)
- `API/Controllers/OrderController.cs` (Refactored)

**Frontend:**
- `Web/src/services/car-service.js` (Updated)
- `Web/src/services/customer-service.js` (Updated)
- `Web/src/services/spare-part-service.js` (Updated)
- `Web/src/services/maintainance-cycle-service.js` (Updated)
- `Web/src/services/repair-service.js` (Updated)

---

## Future Enhancements

- Add `HasMore` flag for infinite scroll pagination
- Implement search debouncing service on frontend
- Add multi-field sorting support
- Create generic PaginatedController base class
- Add pagination to OrderStatus, OrderType, CustomerType endpoints if needed
