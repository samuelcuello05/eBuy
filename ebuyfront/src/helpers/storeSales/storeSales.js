export const sendInStoreSale = async (payload) => {
  try {
    const response = await fetch("http://ebuy.runasp.net/api/InStoreSales/Insert", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });

    const data = await response.json();

    // Si la respuesta es un string plano con el error
    if (typeof data === "string" && data.includes("Customer not found")) {
      throw new Error("Customer not found.");
    }

    // Si la respuesta es un objeto con message
    if (data && typeof data.message === "string" && data.message.includes("Customer not found")) {
      throw new Error("Customer not found.");
    }

    if (!response.ok) {
      throw new Error((data && data.message) || 'Error to send in-store sale order');
    }

    return data;
  } catch (error) {
    console.error("Error sending the sale order:", error.message);
    throw error;
  }
};