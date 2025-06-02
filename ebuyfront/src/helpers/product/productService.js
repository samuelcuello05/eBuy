
const API_BASE_URL = 'http://ebuy.runasp.net/api/Products';

export async function getProducts() {
    try {
        const response = await fetch('http://ebuy.runasp.net/api/products/List');
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}

export async function getProductImages(productId) {
    try {
        const url = `http://ebuy.runasp.net/api/UploadFiles/GetImagesByProductId?IdProduct=${productId}`;
        console.log("📸 Fetching product images:", url);

        const response = await fetch(url);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching product images:', error);
        return [];
    }
}

export async function getProductById(id) {
    const response = await fetch(`${API_BASE_URL}/Search?id=${id}`);
    const data = await response.json();
    return data;
}

