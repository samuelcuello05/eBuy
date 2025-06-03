
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

export async function switchStatusProduct(id) {
    try {
        const url = `http://ebuy.runasp.net/api/OnlineListings/ActivateAndDeactivate?IdOnlineListing=${id}`;
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = await response.json();
        console.log(data);
        return data;
    } catch (error) {
        console.error('Error switching product status:', error);
        return null;
    }
}

export async function getProductsByBranch(branchName) {
    try {
        const response = await fetch(`${API_BASE_URL}/ListByBranchName?branchName=${branchName}`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products by branch:', error);
        return [];
    }
}
