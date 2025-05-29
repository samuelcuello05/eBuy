import Styles from './AdminHome.module.css';

export default function AdminHome() {
    return(
        <section className={Styles["admin-home"]}>
            <h2>Welcome back!</h2>
            <h1 className={Styles["ebuy-admin"]}>eBuy</h1>
            <p className={Styles["ebuy-slogan"]}>"Everything you need, in one place."</p>
        </section>
    );
}