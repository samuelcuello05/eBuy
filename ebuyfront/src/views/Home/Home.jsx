import { useEffect, useState } from 'react';
import { getProducts, getProductImages } from '../../helpers/product/productService';
import TopSellProducts from './components/TopSellProducts/TopSellProducts';
import PrincipalCategories from './components/PrincipalCategories/PrincipalCategories';
import ProductsInterface from '../../components/ProductsInterface/ProductsInterface';
import Topbar from '../../components/Topbar/Topbar';
import Styles from './Home.module.css';

export default function Home() {
    const [products, setProducts] = useState([]);
    const [images, setImages] = useState([]);

useEffect(() => {
    const fetchData = async () => {
        const productsData = await getProducts();

        const imagesPromises = productsData.map(async (product) => {
            const imageData = await getProductImages(product.Name);
            return {
                productName: product.Name,
                images: imageData?.Images || []
            };
        });

        const allImages = await Promise.all(imagesPromises);

        const combined = productsData.map(product => {
            const matched = allImages.find(img => img.productName === product.Name);
            return {
                ...product,
                name: product.Name, 
                description: product.Description,
                images: matched?.images?.map(img => `data:image/jpeg;base64,${img.Content}`) || [],
                status: true
            };
        });

        setProducts(combined);
        setImages(allImages); // opcional, si no lo usas directamente lo puedes omitir
    };

    fetchData();
}, []);

    console.log(products);
    console.log(images);
    return (
        <article className={Styles["home"]}>
            <Topbar />
            <TopSellProducts />
            <PrincipalCategories />
            <ProductsInterface 
                category="home" 
                title="Featured Products" 
                products={products} 
                images={images}
                />
        </article>
    );
}
