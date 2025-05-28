// Employee.jsx
import { Outlet } from "react-router-dom";
import Leftbar from "../../components/Leftbar/Leftbar";
import Styles from "./Employee.module.css";

export default function Employee() {
  return (
    <section className={Styles["employee"]}>
      <Leftbar rol="employee" />
      <div className={Styles["content"]}>
        <Outlet />
      </div>
    </section>
  );
}
