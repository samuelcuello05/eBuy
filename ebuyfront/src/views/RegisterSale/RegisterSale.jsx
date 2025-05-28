import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Styles from './RegisterSale.module.css';
import Button from 'react-bootstrap/Button';

export default function RegisterSale() {
  const [product, setProduct] = useState({
    name: '',
    description: '',
    price: '',
    stock: '',
    brand: '',
    category: '',
  });

  const handleChange = (e) => {
    setProduct({ ...product, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Order placed:', product);
  };

  return (
    <div style={{ padding: '2rem', height: '100vh' }}>
      <h2>💰 Register a Sale</h2>
      <form onSubmit={handleSubmit}>

        <Form.Group className='mb-3' controlId="product">
            <Form.Label>Select Product</Form.Label>
            <Form.Select className={`${Styles["product-select"]} ${Styles["select"]}`} aria-label="Default select example">
            <option>Select a Product</option>
            <option value="1">One</option>
            <option value="2">Two</option>
            <option value="3">Three</option>
            </Form.Select>
        </Form.Group>

        <Form.Group className="mb-3" controlId="quantity">
            <Form.Label>Quantity</Form.Label>
            <Form.Control type="number" placeholder="Enter the quantity" />
        <hr />
        <h2>$299</h2>
        </Form.Group>
            <Form.Group className="mb-3" controlId="button">
            <Button style={{width: "100%",marginTop: "20px"  }} as="input" type="submit" value="Confirm"/>
        </Form.Group>
      </form>
    </div>
  );
}
