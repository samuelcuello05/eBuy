import { useState } from 'react';
import Carousel from 'react-bootstrap/Carousel';
import skinCareImage from '../../../../images/skincare.png'
import consolesImages from '../../../../images/console.png'
import gadgets from '../../../../images/gadgets.png'
import Styles from './TopSellProducts.module.css';
 
function TopSellProducts() {
  const [index, setIndex] = useState(0);
 
  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };
 
  return (
    <div className={Styles["top-sell-products"]}>
        
    
    <Carousel className={Styles["carousel"]} activeIndex={index} onSelect={handleSelect}>
      <Carousel.Item className={Styles["carousel-item"]}>
        <img src={skinCareImage} alt="" className={Styles["carousel-item-image"]}/>
        <Carousel.Caption>
          <h3>First slide label</h3>
          <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
        </Carousel.Caption>
      </Carousel.Item >
      <Carousel.Item className={Styles["carousel-item"]}>
        <img src={consolesImages} alt="" className={Styles["carousel-item-image"]}/>
        <Carousel.Caption>
          <h3>Second slide label</h3>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img src={gadgets} alt="" className={Styles["carousel-item-image"]}/>
        <Carousel.Caption>
          <h3>Third slide label</h3>
          <p>
            Praesent commodo cursus magna, vel scelerisque nisl consectetur.
          </p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
    </div>
  );
}
 
export default TopSellProducts;