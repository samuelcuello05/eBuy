import './App.css';
import PublishProduct from './views/PublishProduct/PublishProduct';
import Home from './views/Home/Home';
import Employee from './views/Employee/Employee';
import PlaceOrder from './views/PlaceOrder/PlaceOrder.jsx';
import RegisterSale from './views/RegisterSale/RegisterSale.jsx';
import ViewProducts from './views/ViewProducts/ViewProducts.jsx';
import Supplier from './views/Supplier/Supplier.jsx';
import AdminHome from './components/AdminHome/AdminHome.jsx';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home/>} />

        <Route path="/employee" element={<Employee />}>
          <Route index element={<Navigate to="home" replace />} />
          <Route path="home" element={<AdminHome />} />
          <Route path="publish" element={<PublishProduct />} />
          <Route path="place-order" element={<PlaceOrder />} />
          <Route path="register-sale" element={<RegisterSale />} />
          <Route path="view-products" element={<ViewProducts />} />
        </Route>

        <Route path="/supplier" element={<Supplier />}>
          <Route index element={<Navigate to="home" replace />} />
          <Route path="home" element={<AdminHome />} />
          <Route path="publish" element={<PublishProduct />} />
          <Route path="view-products" element={<ViewProducts />} />
        </Route>
      </Routes>
  </Router>
  );
}

export default App;
