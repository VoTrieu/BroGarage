import { useNavigate } from "react-router-dom";

const stats = [
  {
    label: "Active repairs",
    value: "12",
    icon: "pi pi-cog",
    tone: "teal",
  },
  {
    label: "Vehicles in bay",
    value: "8",
    icon: "pi pi-car",
    tone: "blue",
  },
  {
    label: "Parts low stock",
    value: "5",
    icon: "pi pi-box",
    tone: "amber",
  },
  {
    label: "Ready to deliver",
    value: "3",
    icon: "pi pi-check-circle",
    tone: "slate",
  },
];

const HomePage = () => {
  const navigate = useNavigate();

  return (
    <section className="home-dashboard">
      <div className="home-hero">
        <div>
          <p className="home-eyebrow">Bro Garage</p>
          <h1>Premium garage operations dashboard</h1>
          <p className="home-copy">
            Track repair orders, customers, vehicles, maintenance cycles, and spare parts from one workspace.
          </p>
          <div className="home-hero-actions">
            <button type="button" onClick={() => navigate("/app/repair-detail/new")}>
              <span className="pi pi-plus"></span>
              New repair order
            </button>
            <button type="button" onClick={() => navigate("/app/repair")}>
              <span className="pi pi-search"></span>
              Search vehicle
            </button>
          </div>
        </div>
        <div className="home-hero-panel">
          <img src="/images/garage-hero.svg" alt="" />
          <div>
            <strong>Today</strong>
            <p>Prioritize inspections, pending quotes, and delivery-ready vehicles.</p>
          </div>
        </div>
      </div>

      <div className="home-stat-grid">
        {stats.map((item) => (
          <article className={`home-stat home-stat-${item.tone}`} key={item.label}>
            <span className={item.icon}></span>
            <div>
              <strong>{item.value}</strong>
              <p>{item.label}</p>
            </div>
          </article>
        ))}
      </div>

      <div className="home-work-grid">
        <article>
          <h2>Fast actions</h2>
          <div className="home-action-row">
            <span className="pi pi-plus-circle"></span>
            <div>
              <strong>Create repair order</strong>
              <p>Start a quote or open a repair ticket for an existing vehicle.</p>
            </div>
          </div>
          <div className="home-action-row">
            <span className="pi pi-user-plus"></span>
            <div>
              <strong>Add customer</strong>
              <p>Register customer details and attach vehicles to their profile.</p>
            </div>
          </div>
          <div className="home-action-row">
            <span className="pi pi-file-excel"></span>
            <div>
              <strong>Export reports</strong>
              <p>Download table data for handoff, accounting, and inventory checks.</p>
            </div>
          </div>
        </article>

        <article>
          <h2>Service flow</h2>
          <ol className="home-flow">
            <li>Receive vehicle and record customer notes.</li>
            <li>Inspect, quote, and approve required work.</li>
            <li>Assign parts, labor, payment, and delivery status.</li>
          </ol>
        </article>
      </div>
    </section>
  );
};

export default HomePage;
