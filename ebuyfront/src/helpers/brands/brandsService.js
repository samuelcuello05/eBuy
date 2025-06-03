const BASE_URL = 'http://ebuy.runasp.net/api/'

export async function getBrands() {
    try {
        const response = await fetch(`${BASE_URL}Brands/List`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}