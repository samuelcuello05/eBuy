import Topbar from "../../components/Topbar/Topbar";
import Styles from "./Cart.module.css";
import Subtitle from "../../components/Subtitle/Subtitle";
import CartItem from "./Components/CartItem/CartItem";
import { productos } from "../../products";
import { useState } from "react";
import { Form, Button, Container } from "react-bootstrap";

export default function Cart() {
  const [paymentMethod, setPaymentMethod] = useState("credit");

  const total = productos.reduce((sum, p) => sum + p.price, 0).toFixed(2);

  return (
    <article className={Styles["cart"]}>
      <Topbar />
      <section className={Styles["cart-container"]}>
        <div className={Styles["subtitle"]} >
         <Subtitle text={"Cart"} />
        </div>
        
        <Container className="py-3" style={{ maxWidth: "800px" }}>
          {productos.map((product, index) => (
            product.status && <CartItem key={index} product={product} />
          ))}

          <div className={`${"mt-4 p-3 border rounded bg-dark"}`} id={Styles["payment"]}>
            <h5>Total: <span className="fw-bold">${total}</span></h5>
            <Form.Group controlId="paymentSelect" className={`${"my-3"}`}>
              <Form.Label>Select Payment Method</Form.Label>
              <Form.Select
                value={paymentMethod}
                onChange={(e) => setPaymentMethod(e.target.value)}
                className={Styles["select"]}
              >
                <option value="credit">Credit Card</option>
                <option value="paypal">PayPal</option>
                <option value="cash">Cash on Delivery</option>
              </Form.Select>
            </Form.Group>
            <Button variant="success" size="lg" className="w-100">Pay Now</Button>
          </div>
        </Container>
      </section>
    </article>
  );
}
