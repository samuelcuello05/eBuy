import { useEffect, useState } from 'react';
import { getProducts, getProductImages } from '../../helpers/product/productService';
import { getOnlineListing } from '../../helpers/product/onlineLIsting';
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
        const productsData = await getOnlineListing();

        const imagesPromises = productsData.map(async (product) => {
            const imageData = await getProductImages(product.IdProduct);
            return {
                idProduct: product.IdProduct, // 👈 aquí está la solución
                images: imageData?.Images || []
            };
        });

        const allImages = await Promise.all(imagesPromises);

        const combined = productsData.map(product => {
            const matched = allImages.find(img => img.idProduct === product.IdProduct);
            return {
                ...product,
                name: product.Title,
                description: product.Description,
                price: product.Price,
                images: matched?.images?.map(img => `data:image/jpeg;base64,${img.Content}`) || [],
                status: true
            };
        });

        setProducts(combined);
      
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
                />
        </article>
    );
}
