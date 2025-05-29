import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import cart from '../../images/cart.svg';
import Styles from './Topbar.module.css';
import { ReactComponent as CartIcon } from '../../images/cart.svg';
import { NavLink, resolvePath } from "react-router-dom";


function Topbar() {
    //Change this component when the user is logged in to show his name and change their view
    let price = 100;
  return (
    <nav className={Styles["topbar"]}>
        <div className={Styles["topbar-content"]}>
            <div className={Styles["first-container"]}>
                <h1 className={Styles["eBuy"]}>eBuy</h1>

                <NavDropdown title="Categories" id="basic-nav-dropdown" className={Styles["categories-dropdown"]}>
                    <NavDropdown.Item href="#category-1">Cateory 1</NavDropdown.Item>
                    <NavDropdown.Item href="#category-2">Category 2</NavDropdown.Item>
                    <NavDropdown.Item href="#category-3">Category 3</NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item href="#all-categories">See all Categories</NavDropdown.Item>
                </NavDropdown>
            </div>
            <div className={Styles["last-container"]} >
                <NavLink to={"/login" } className={Styles["log-in"]}>
                    <p>Log in</p>
                </NavLink>
                <div className={Styles["cart-container"]}>
                <CartIcon className={Styles["cart-icon"]} />
                    
                        <p className={Styles["price"]}>{price>99 ? (99+"+"):price}</p>
                    
                </div>
                 
              
            </div>
        </div>    
    </nav>

    //   <Navbar bg="dark" data-bs-theme="light" className={Styles["topbar"]} expand="lg">
    //     <Container>
    //       <Navbar.Brand href="#home" className={Styles["eBuy"]}>eBuy</Navbar.Brand>
    //       <Nav className="me-auto">

    //         <NavDropdown title="Categories" id="basic-nav-dropdown">
    //           <NavDropdown.Item href="#category-1">Cateory 1</NavDropdown.Item>
    //           <NavDropdown.Item href="#category-2">Category 2</NavDropdown.Item>
    //           <NavDropdown.Item href="#category-3">Category 3</NavDropdown.Item>
    //           <NavDropdown.Divider />
    //           <NavDropdown.Item href="#all-categories">See all Categories</NavDropdown.Item>
    //         </NavDropdown>
    //       </Nav>
    //       <Navbar.Collapse className="justify-content-end">
    //       <Navbar.Text>
    //         <a href="#cart" className={Styles["cart-link"]}>
    //         <img src={cart} alt="" className={Styles["cart"]} />
    //         </a>
    //       </Navbar.Text>
    //       <Navbar.Text>
    //         <a href="#login" className={Styles["log-in"]}>Log in</a>
    //       </Navbar.Text>
    //     </Navbar.Collapse>
    //     </Container>
    //   </Navbar>
  );
}

export default Topbar;