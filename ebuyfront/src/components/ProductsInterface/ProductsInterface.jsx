import Subtitle from "../Subtitle/Subtitle";
import Styles from "./ProductsInterface.module.css";
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import skinCareImage from '../../images/skincare.png'
import consolesImages from '../../images/console.png'
import gadgets from '../../images/gadgets.png'

//When the .NET API is ready, start to create helpers to fetch the products by category
export default function ProductsInterface({category}) {
    return(
        <section className={Styles["products-interface"]}>
            <Subtitle text={category}/>
            <Products category={category}/>
        </section>
    );
}

function Products({category}){ //This array should be replaced by a fetch to the .NET API
    let products = [
        {name: "Product 1", image: skinCareImage, description: "This is a great product for your skin."},
        {name: "Product 2", image: consolesImages, description: "Experience the next generation of gaming with this console."},
        {name: "Product 3", image: gadgets, description: "Innovative gadgets to make your life easier."},
        {name: "Product 4", image: skinCareImage, description: "This is a great product for your skin."},
        {name: "Product 5", image: consolesImages, description: "Experience the next generation of gaming with this console."},
        {name: "Product 6", image: gadgets, description: "Innovative gadgets to make your life easier."},
        {name: "Product 7", image: skinCareImage, description: "This is a great product for your skin."},
        {name: "Product 8", image: consolesImages, description: "Experience the next generation of gaming with this console."},
        {name: "Product 9", image: gadgets, description: "Innovative gadgets to make your life easier."},
        {name: "Product 10", image: skinCareImage, description: "This is a great product for your skin."},
        {name: "Product 11", image: consolesImages, description: "Experience the next generation of gaming with this console."},
        {name: "Product 12", image: gadgets, description: "Innovative gadgets to make your life easier."},
    ];
    return(
    <section className={Styles["products-container"]}>
        {products.map((product, index) => (
            <Card key={index} style={{ width: '18rem' }} className={Styles["product-card"]}>
                <Card.Img variant="top" src={product.image} />
                <Card.Body className={Styles["card-body"]}>
                    <Card.Title>{product.name}</Card.Title>
                    <Card.Text>
                        {product.description}
                    </Card.Text>
                    <p className={Styles["price"]}>$299</p>
                    <Button variant="outline-warning" className={Styles["add-button"]} >Add to cart</Button>
                </Card.Body>
            </Card>
        ))}
    </section>

    );
}