import { Card, Button, Row, Col } from "react-bootstrap";
import styles from "./CartItem.module.css";

export default function CartItem({ product }) {
  return (
    <Card className={`${styles["card"]} ${"mb-3 w-100"}`}>
      <Row className={` ${styles["row"]} ${"g-0 align-items-center p-2"}`}>
        <Col md={2}>
          <Card.Img src={product.images[1]} alt={product.name} className={styles["img"]} />
        </Col>
        <Col md={6}>
          <Card.Body>
            <Card.Title className="mb-1">{product.name}</Card.Title>
            <Card.Text className={`${styles["seller"]}`}>Sold by: TechStore Inc.</Card.Text>
          </Card.Body>
        </Col>
        <Col md={2}>
          <Card.Body>
            <Card.Text className="fw-bold fs-5">${product.price}</Card.Text>
          </Card.Body>
        </Col>
      </Row>
    </Card>
  );
}
