import { Card, Button, Row, Col, Form } from "react-bootstrap";
import styles from "./CartItem.module.css";

export default function CartItem({ product, onQuantityChange, onRemove }) {
  return (
    <Card className={`${styles["card"]} mb-3 w-100`}>
      <Row className={`${styles["row"]} g-0 align-items-center p-2`}>
        <Col md={2}>
          <Card.Img src={product.images[1]} alt={product.name} className={styles["img"]} />
        </Col>
        <Col md={5}>
          <Card.Body>
            <Card.Title className="mb-1">{product.name}</Card.Title>
            <Card.Text className={styles["seller"]}>Sold by: TechStore Inc.</Card.Text>
          </Card.Body>
        </Col>
        <Col md={1}>
          <Form.Control
            type="number"
            min={1}
            value={product.quantity}
            className={styles["quantity-input"]}
            onChange={(e) => onQuantityChange(product.id, parseInt(e.target.value))}
          />
        </Col>
        <Col md={2}>
          <Card.Body>
            <Card.Text className="fw-bold fs-5">${(product.price * product.quantity).toFixed(2)}</Card.Text>
          </Card.Body>
        </Col>
        <Col md={2}>
          <Button variant="danger" onClick={() => onRemove(product.id)}>Remove</Button>
        </Col>
      </Row>
    </Card>
  );
}
