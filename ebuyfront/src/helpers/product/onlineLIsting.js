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

export async function getPublisherName(id) {
        try {
        const response = await fetch(`${API_BASE_URL}GetOnlineListingPublisherName?idOnlineListing=${id}`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}

export async function getListingBySupplier(id){
    try {
        const response = await fetch(`http://ebuy.runasp.net/api/OnlineListingBySuppliers/List?IdSupplier=${id}`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}

export async function getListingByOwn(){
    try {
        const response = await fetch(`http://ebuy.runasp.net/api/OnlineListingOwns/List`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}

