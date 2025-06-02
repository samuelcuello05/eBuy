const API_BASE_URL = 'http://ebuy.runasp.net/api/OnlineListings/';

export async function getOnlineListing() {
    try {
        const response = await fetch(`${API_BASE_URL}List`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}