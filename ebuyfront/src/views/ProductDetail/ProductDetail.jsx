import Styles from './ProductDetail.module.css';
import { useParams } from 'react-router-dom';
import { productos } from '../../products';
import { useState } from 'react';
import Topbar from '../../components/Topbar/Topbar';
import ProductImage from './components/ProductImages/ProductImages';

export default function ProductDetail() {
    const { id } = useParams();
    const [product, setProduct] = useState(
        productos.find(p => p.id === parseInt(id))
    ); 
    if (!product) {
        return <h1>Product not found</h1>;
    }
    return(
        <article className={Styles["product-detail"]}>
            <Topbar />
            <section className={Styles["content"]}>
                 <ProductImage productInformation={product.images} />  
            </section>
           
        </article>
    );

}