const BASE_URL = 'http://ebuy.runasp.net/api/'

export async function getCategories() {
    try {
        const response = await fetch(`${BASE_URL}Categories/List`);
        const data = await response.json();
        console.log('Fetched categories:', data);
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}