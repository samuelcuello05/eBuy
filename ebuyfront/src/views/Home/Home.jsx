import TopSellProducts from './components/TopSellProducts/TopSellProducts';
import PrincipalCategories from './components/PrincipalCategories/PrincipalCategories';
import ProductsInterface from '../../components/ProductsInterface/ProductsInterface';
import Topbar from '../../components/Topbar/Topbar';
import Styles from './Home.module.css';

export default function Home() {
    return(
        <article className={Styles["home"]}>
            <Topbar/>
            <TopSellProducts/>
            <PrincipalCategories/>
            <ProductsInterface category="For You"/>   
        </article>
    );
}