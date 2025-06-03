const BASE_URL = 'http://ebuy.runasp.net/api/'

export async function getSuppliers() {
    try {
        const response = await fetch(`${BASE_URL}Suppliers/List`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}