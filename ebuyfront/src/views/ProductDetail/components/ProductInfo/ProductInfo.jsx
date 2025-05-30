import Styles from './ProductInfo.module.css';
import Button from 'react-bootstrap/esm/Button';

export default function ProductInfo({ product }) {
    return (
        <div className={Styles["product-info"]}>
            <h1 className={Styles["product-title"]}>{product.name}</h1>
            <p className={Styles["product-description"]}>{product.description} Lorem, ipsum dolor sit amet consectetur adipisicing elit. Asperiores saepe nemo eius eaque vel voluptas quaerat. Vero quo facilis, voluptatibus saepe repudiandae adipisci iste molestias reprehenderit eos, numquam impedit libero.</p>
            
            <div className={Styles["price-container"]}>
                <h2 className={Styles["price"]}>${product.price}</h2>
                <p className={Styles["product-seller"]}>Vendido por Daniel Marin</p>
            </div>

            <Button className={Styles["add-to-cart-button"]}  size='lg'>
                Add to Cart
            </Button>
        </div>
    );
}