import Styles from './ProductDetail.module.css';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import Topbar from '../../components/Topbar/Topbar';
import ProductImage from './components/ProductImages/ProductImages';
import ProductInfo from './components/ProductInfo/ProductInfo';
import { getProductByName, getProductImages } from '../../helpers/product/productService';


function decodeNameFromURL(urlParam) {
  try {
    return decodeURIComponent(urlParam.replace(/-/g, ' '));
  } catch (e) {
    console.error("❌ Error decoding URL:", e);
    return urlParam;
  }
}


export default function ProductDetail() {
   const { productName } = useParams();
  const [product, setProduct] = useState(null);

  useEffect(() => {
    const fetchProduct = async () => {
      const decodedName = decodeNameFromURL(productName);
      console.log('🔍 Decoded product name from URL:', decodedName);

      const productFromApi = await getProductByName(decodedName);
      console.log('📦 Producto retornado por el backend:', productFromApi);

      if (!productFromApi) {
        console.warn('⚠️ No se encontró el producto con nombre:', decodedName);
      }

      setProduct(productFromApi);
    };

    fetchProduct();
  }, [productName]);

  if (!product) return <div>Producto no encontrado.</div>;

    return (
        <article className={Styles["product-detail"]}>
            <Topbar />
            <section className={Styles["content"]}>
                <ProductImage productInformation={product.images} />
                <ProductInfo product={product} />
            </section>
        </article>
    );
}
