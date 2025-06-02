import Styles from './ProductDetail.module.css';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import Topbar from '../../components/Topbar/Topbar';
import ProductImage from './components/ProductImages/ProductImages';
import ProductInfo from './components/ProductInfo/ProductInfo';
import { getProductById, getProductImages } from '../../helpers/product/productService';
import { getOnlineListing } from '../../helpers/product/onlineLIsting';

export default function ProductDetail() {
    const { id } = useParams();
    const [product, setProduct] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchProduct = async () => {
            const listenings = await getOnlineListing();
            const productData = listenings.find(product => product.Id === parseInt(id));
            const imageData = await getProductImages(productData.IdProduct);

            const processedProduct = {
                ...productData,
                name: productData.Title,
                description: productData.Description,
                images: imageData?.Images?.map(img => `data:image/jpeg;base64,${img.Content}`) || []
            };

            setProduct(processedProduct);
            setLoading(false);
        };

        fetchProduct();
    }, [id]);

    if (loading) return <h1>Loading...</h1>;
    if (!product) return <h1>Product not found</h1>;

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
