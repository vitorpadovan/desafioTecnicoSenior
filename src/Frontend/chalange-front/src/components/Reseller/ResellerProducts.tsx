"use client";
import React, { useEffect, useState } from "react";
import { ProductService } from "../../api-services/product-services";
import { Product } from "../../interfaces/Product";

export default function ResellerProducts(params: { readonly resellerId: string }) {
  const [products, setProducts] = useState<Product[]>([]);
  const [cart, setCart] = useState<{ [key: number]: number }>({});

  useEffect(() => {
    async function fetchProducts() {
      try {
        const data = await ProductService.getProductsFromReseller(params.resellerId);
        setProducts(data);
      } catch (error) {
        console.error("Erro ao buscar produtos do revendedor:", error);
      }
    }
    fetchProducts();
  }, [params.resellerId]);

  const handleAdd = (productId: number) => {
    setCart((prevCart) => ({
      ...prevCart,
      [productId]: (prevCart[productId] || 0) + 1,
    }));
  };

  const handleSubtract = (productId: number) => {
    setCart((prevCart) => ({
      ...prevCart,
      [productId]: Math.max((prevCart[productId] || 0) - 1, 0),
    }));
  };

  const handlePurchase = () => {
    const orderDetails = Object.entries(cart)
      .filter(([_, quantity]) => quantity > 0)
      .map(([productId, quantity]) => ({
        productId: Number(productId),
        quantity,
      }));
    console.log({ orderDetails });
    alert("Pedido gerado! Verifique o console para os detalhes.");
    setCart({}); // Limpa as quantidades no carrinho
  };

  return (
    <div className="flex justify-center mt-8">
      <div className="w-4/5">
        <h1 className="text-center text-2xl font-bold mb-4">Produtos do Revendedor</h1>
        <table className="table-auto w-full border-collapse border border-gray-300 dark:border-gray-700">
          <thead>
            <tr className="bg-gray-200 dark:bg-gray-800">
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">Nome</th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">Descrição</th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">Preço</th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">Quantidade</th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">Ações</th>
            </tr>
          </thead>
          <tbody>
            {products.map((product) => (
              <tr key={product.id} className="text-center">
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">{product.name}</td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">{product.description}</td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">{product.price}</td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {cart[product.id] || 0}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  <button
                    className="bg-green-500 text-white px-2 py-1 rounded mr-2"
                    onClick={() => handleAdd(product.id)}
                  >
                    +
                  </button>
                  <button
                    className="bg-red-500 text-white px-2 py-1 rounded"
                    onClick={() => handleSubtract(product.id)}
                  >
                    -
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <div className="flex justify-end mt-4">
          <button
            className="bg-blue-500 text-white px-6 py-2 rounded hover:bg-blue-600"
            onClick={handlePurchase}
          >
            Comprar
          </button>
        </div>
      </div>
    </div>
  );
}
