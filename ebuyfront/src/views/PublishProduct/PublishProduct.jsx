
import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Styles from './PublishProduct.module.css';
import Button from 'react-bootstrap/Button';

export default function PublishProduct() {
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
    console.log('Producto enviado:', product);
  };

  return (
    <div style={{ padding: '2rem', height: '100%' }}>
      <h2>📦 Publish a New Product</h2>
      <form onSubmit={handleSubmit}>
      <Form.Group className='mb-3' controlId="category">
        <Form.Label>Select Category</Form.Label>
        <Form.Select className={`${Styles["category-select"]} ${Styles["select"]}`} aria-label="Default select example">
          <option>Select a Category</option>
          <option value="1">One</option>
          <option value="2">Two</option>
          <option value="3">Three</option>
        </Form.Select>
      </Form.Group>

      <Form.Group className='mb-3' controlId="brand">
        <Form.Label>Select Brand</Form.Label>
        <Form.Select className={`${Styles["brand-select"]} ${Styles["select"]}`} aria-label="Default select example">
          <option>Select a Brand</option>
          <option value="1">One</option>
          <option value="2">Two</option>
          <option value="3">Three</option>
        </Form.Select>
      </Form.Group>

      <Form.Group className="mb-3" controlId="name">
        <Form.Label>Product Name</Form.Label>
        <Form.Control type="text" placeholder="Enter a name" required/>
      </Form.Group>

      
      <Form.Group className="mb-3" controlId="description">
        <Form.Label>Product Description</Form.Label>
        <Form.Control type="text" placeholder="Enter a Description" />
      </Form.Group>

       <Form.Group controlId="images" className="mb-3">
        <Form.Label>Upload product images</Form.Label>
        <Form.Control type="file" multiple accept="image/png, image/jpeg" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="costPrice">
        <Form.Label>Cost price</Form.Label>
        <Form.Control type="number" placeholder="Enter the cost price" required/>
      </Form.Group>

      
      <Form.Group className="mb-3" controlId="salePrice">
        <Form.Label>Sale price</Form.Label>
        <Form.Control type="number" placeholder="Enter the sale price" required/>
      </Form.Group>

      <Form.Group className="mb-3" controlId="stock">
        <Form.Label>Stock</Form.Label>
        <Form.Control type="number" placeholder="Enter the stock" required/>
      </Form.Group>

      <Form.Group className="mb-3" controlId="date">
      <Form.Label>Date</Form.Label>
        <Form.Control type="month" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="button">
       <Button style={{width: "100%",marginTop: "20px"  }} as="input" type="submit" value="Submit"/>
      </Form.Group>
      
      </form>
    </div>
  );
}
