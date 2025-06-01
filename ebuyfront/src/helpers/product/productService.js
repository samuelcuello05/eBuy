
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

export async function getProductImages(productName) {
    try {
        const url = `http://ebuy.runasp.net/api/UploadFiles/GetImagesByProduct?productName=${encodeURIComponent(productName)}`;
        console.log("📸 Fetching product images:", url);

        const response = await fetch(url);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching product images:', error);
        return [];
    }
}


export async function getProductByName(name) {
  const encodedName = encodeURIComponent(name.trim());
  const url = `${API_BASE_URL}/Search?name=${encodedName}`;
  console.log('🌐 Fetching product by name:', url);

  try {
    const response = await fetch(url);

    if (!response.ok) {
      console.error('❌ Error en la respuesta del servidor:', response.status, response.statusText);
      return null;
    }

    const product = await response.json();
    console.log('✅ Producto recibido del backend:', product);

    return product;
  } catch (error) {
    console.error('❌ Error fetching product:', error);
    return null;
  }
}
