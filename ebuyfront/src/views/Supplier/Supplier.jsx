import { Outlet } from "react-router-dom";
import Leftbar from "../../components/Leftbar/Leftbar";
import Styles from "./Supplier.module.css";

export default function supplier() {
  return (
    <section className={Styles["supplier"]}>
      <Leftbar rol="supplier" />
      <div className={Styles["content"]}>
        <Outlet />
      </div>
    </section>
  );
}
