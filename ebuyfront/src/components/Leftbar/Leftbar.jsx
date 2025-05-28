// Leftbar.jsx
import { NavLink } from "react-router-dom";
import Styles from "./Leftbar.module.css";

export default function Leftbar({ rol }) {
  const menuItems = rol === "employee"
    ? [
        { id: 'publish', label: '📦 Publish Product' },
        { id: 'place-order', label: '🛒 Place an order' },
        { id: 'register-sale', label: '💰 Register a Sale' },
        { id: 'view-products', label: '👀 View Products' },
      ]
    : [
        { id: 'publish', label: '📦 Publish a Product' },
        { id: 'view-products', label: '👀 View Products' },
      ];

  return (
    <aside className={Styles["leftbar"]}>
      <div className="d-flex flex-column p-3 text-white" style={{ height: '100vh', backgroundColor: 'rgb(47, 50, 63)' }}>
        <h5 className="mb-4">Employee Menu</h5>
        <ul className="list-unstyled">
          {menuItems.map(({ id, label }) => (
            <li key={id}>
              <NavLink
                to={`/employee/${id}`}
                className={({ isActive }) =>
                  `${Styles.linkItem} ${isActive ? Styles.active : ""}`
                }
              >
                {label}
              </NavLink>
            </li>
          ))}
        </ul>
      </div>
    </aside>
  );
}
