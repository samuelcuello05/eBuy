import Image from 'react-bootstrap/Image';
import skinCareImage from '../../../../images/skincare.png'
import consolesImages from '../../../../images/console.png'
import gadgets from '../../../../images/gadgets.png'
import Styles from './PrincipalCategories.module.css';
import Subtitle from '../../../../components/Subtitle/Subtitle';

function PrincipalCategories() {
  return (
    <section className={Styles["principal-categories"]}>     
        <Subtitle text="Our Principal Categories" />   
        <div className={Styles["categories-container"]}>
            <div className={Styles["category-item"]}>
                <Image className={Styles["rounded-image"]} src={skinCareImage} roundedCircle />
                <p>Category 1</p>
            </div>
 
            <div className={Styles["category-item"]}>
                <Image className={Styles["rounded-image"]} src={consolesImages} roundedCircle />
                <p>Category 2</p>
            </div>
            <div className={Styles["category-item"]}>
                <Image className={Styles["rounded-image"]} src={skinCareImage} roundedCircle />
                <p>Category 3</p>
            </div>
            <div className={Styles["category-item"]}>
                <Image className={Styles["rounded-image"]} src={gadgets} roundedCircle />
                <p>Category 4</p>
            </div>
            <div className={Styles["category-item"]}>
                <Image className={Styles["rounded-image"]} src={skinCareImage} roundedCircle />
                <p>Category 5</p>
            </div>

            <div className={Styles["category-item"]}>
                <Image className={Styles["rounded-image"]} src={gadgets} roundedCircle />
                <p>Category 6</p>
            </div>
        </div>

    </section>
    );

}
export default PrincipalCategories;