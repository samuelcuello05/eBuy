import Subtitle from "../Subtitle/Subtitle";
import Styles from "./ProductsInterface.module.css";
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { useState } from "react";
import { NavLink, resolvePath } from "react-router-dom";

export default function ProductsInterface({category, title, products}) {
    return(
        <section className={Styles["products-interface"]}>
            <Subtitle text={title}/>
            <Products category={category} products={products}/>
        </section>
    );
}

function Products({category, products}){ //This array should be replaced by a fetch to the .NET API
    
    function handleInactivateSale(index) {
        alert("Sale inactivated successfully!");
        setProducts(prevProducts =>{
            const updatedProducts = [...prevProducts];
            updatedProducts[index].status = false;
            return updatedProducts;
        })
    }

    function handlePutOnSale(index) {
        alert(`Product ${index + 1} is now on sale!`);
        setProducts(prevProducts => {
            const updatedProducts = [...prevProducts];
            updatedProducts[index].status = true;
            return updatedProducts;
        });
    }
    const [productss, setProducts] = useState();
        
    if(category === "employee" || category === "supplier"){
        return(
        <section className={Styles["products-container"]}>
            {products.map((product, index) => (
                <Card key={index} style={{ width: '18rem' }} className={Styles["product-card"]}>
                    <Card.Img
                        variant="top"
                        src={product.images[0] || "https://via.placeholder.com/300x200?text=No+Image"}
                    />

                    <Card.Body className={Styles["card-body"]}>
                        <Card.Title>{product.name}</Card.Title>
                        <Card.Text>
                            {product.Description}
                        </Card.Text>
                        <p className={Styles["price"]}>${product.price}</p>
                        {product.status 
                            ? <Button variant="outline-danger" className={Styles["inactivate-sale"]} onClick={() => handleInactivateSale(index)} >Inactivate sale</Button>
                            : <Button variant="outline-secondary" className={Styles["inactivate-sale"]} onClick={() => handlePutOnSale(index)} >Put on sale</Button>}
                    </Card.Body>
                </Card>
            ))}
        </section>
        );
    }
    return(
    <section className={Styles["products-container"]}>
        {products.map((product, index) => (
            product.status &&(
            <Card key={index} style={{ width: '18rem' }} className={Styles["product-card"]}>
                        <Card.Img
                        variant="top"
                        src={product.images[0] || "https://via.placeholder.com/300x200?text=No+Image"
                        }
                    />
                <Card.Body className={Styles["card-body"]}>
                    <Card.Title>{product.name}</Card.Title>
                    <Card.Text>
                        {product.Description}
                    </Card.Text>
                        <p className={Styles["price"]}>${product.price}</p>
                        <NavLink to={`/product/${product.Id}`}>
                            <Button variant="outline-warning" className={Styles["add-button"]}>Add to cart</Button>
                        </NavLink>
                </Card.Body>
            </Card>)
        ))}
    </section>

    );
}