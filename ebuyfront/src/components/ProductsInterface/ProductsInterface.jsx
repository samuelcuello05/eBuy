import Subtitle from "../Subtitle/Subtitle";
import Styles from "./ProductsInterface.module.css";
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { useState, useEffect } from "react";
import { NavLink, resolvePath } from "react-router-dom";
import { switchStatusProduct } from "../../helpers/product/productService";

export default function ProductsInterface({category, title, products}) {
    return(
        <section className={Styles["products-interface"]}>
            <Subtitle text={title}/>
            <Products category={category} products={products}/>
        </section>
    );
}

function Products({category, products}){ //This array should be replaced by a fetch to the .NET API
    const [localProducts, setLocalProducts] = useState(products);
       useEffect(() => {
        setLocalProducts(products);
    }, [products]);

async function handleActiveSwitch(index) {
    await switchStatusProduct(localProducts[index].IdOnlineListing);
    setLocalProducts(prev => {
        const updated = [...prev];
        updated[index] = {
            ...updated[index],
            status: !updated[index].status 
        };
        return updated;
    });
}

    function handlePutOnSale(index) {
        alert(`Product ${index + 1} is now on sale!`);
    }
    const [productss, setProducts] = useState();
        
    if(category === "Employee" || category === "Supplier"){
        return(
        <section className={Styles["products-container"]}>
            {localProducts.map((product, index) => (
                <Card key={index} style={{ width: '18rem' }} className={Styles["product-card"]}>
                    <Card.Img
                        variant="top"
                        src={product.images[0] || "https://via.placeholder.com/300x200?text=No+Image"}
                    />

                    <Card.Body className={Styles["card-body"]}>
                        <Card.Title id={Styles["title"]}>{product.name}</Card.Title>
                        <Card.Text>
                            {product.Description}
                        </Card.Text>
                        <p className={Styles["price"]}>${product.price}</p>
                        {product.status 
                            ? <Button variant="outline-danger" className={Styles["inactivate-sale"]} onClick={() => handleActiveSwitch(index)} >Inactivate sale</Button>
                            : <Button variant="outline-secondary" className={Styles["inactivate-sale"]} onClick={() => handleActiveSwitch(index)} >Put on sale</Button>}
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
                            <Button variant="outline-warning" className={Styles["add-button"]}>View product</Button>
                        </NavLink>
                </Card.Body>
            </Card>)
        ))}
    </section>

    );
}