import Styles from './ProductInfo.module.css';
import Button from 'react-bootstrap/esm/Button';

export default function ProductInfo({ product }) {
    return (
        <div className={Styles["product-info"]}>
            <h1 className={Styles["product-title"]}>{product.name}</h1>
            <p className={Styles["product-description"]}>{product.description}</p>
            
            <div className={Styles["price-container"]}>
                <h2 className={Styles["price"]}>${product.Price}</h2>
                <p className={Styles["product-seller"]}>Vendido por Daniel Marin</p>
            </div>

            <Button className={Styles["add-to-cart-button"]}  size='lg'>
                Add to Cart
            </Button>
        </div>
    );
}