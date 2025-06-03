import { API_BASE_URL } from "../variables.js";

export const getCart = async (Id) => {
    try {
        const response = await fetch(`${API_BASE_URL}Carts/ListCartProducts?idCustomer=${Id}`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching cart products:', error);
        return [];
    }
}

export const makeOnlineSale = async (payload) =>{
    try {
    const response = await fetch(`${API_BASE_URL}OnlineSales/Insert`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });

    const data = await response.json();

    if (!response.ok) {
      throw new Error((data && data.message) || 'Error to send online sale order');
    }

    return data;
  } catch (error) {
    console.error("Error sending the sale order:", error.message);
    throw error;
  }
}

export async function clearCart(Id) {
    try {
        const response = await fetch(`${API_BASE_URL}Carts/Clear?idCart=${Id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error('Error clearing the cart');
        }

        return await response.json();
    } catch (error) {
        console.error('Error clearing cart:', error);
        throw error;
    }
}

export async function removeItemCart(idCart, idProduct) {
    try {
        const response = await fetch(`${API_BASE_URL}Carts/RemoveItem?idCart=${idCart}&idProduct=${idProduct}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error('Error clearing the cart');
        }

        return await response.json();
    } catch (error) {
        console.error('Error clearing cart:', error);
        throw error;
    }
}  

export const addToCart = async (customerId, productId, branchName) =>{
    try {
    const response = await fetch(`${API_BASE_URL}Carts/AddItem?idCart=${customerId}&idProduct=${productId}&branchName=${branchName}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
    });

    const data = await response.json();
    console.log("data"  , data);

    if (!response.ok) {
      throw new Error((data && data.message) || 'Error adding item to cart');
    }

    return data;
  } catch (error) {
    console.error("Error adding item to cart:", error.message);
    throw error;
  }
}