import TopSellProducts from './Home/components/TopSellProducts/TopSellProducts';
import PrincipalCategories from './Home/components/PrincipalCategories/PrincipalCategories';
import ProductsInterface from '../components/ProductsInterface/ProductsInterface';
export default function Home() {
    return(
        <article className="Home">
            <TopSellProducts/>
            <PrincipalCategories/>
            <ProductsInterface category="For You"/>   
        </article>
    );
}