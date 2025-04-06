"use client";
import React from "react";
import { useEffect, useState } from "react";
import { ProductService } from "../../api-services/product-services";
import { Product } from "../../interfaces/Product";
import Link from "next/link";

export default function ResellerProducts(params: {
  readonly resellerId: string;
}) {
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    async function fetchProducts() {
      try {
        const data = await ProductService.getProductsFromReseller(
          params.resellerId
        );
        setProducts(data);
      } catch (error) {
        console.error("Erro ao buscar produtos do revendedor:", error);
      }
    }
    fetchProducts();
  }, [params.resellerId]);

  return (
    <div className="flex justify-center mt-8">
      <div className="w-4/5">
        <h1 className="text-center text-2xl font-bold mb-4">
          Produtos do Revendedor
        </h1>
        <table className="table-auto w-full border-collapse border border-gray-300 dark:border-gray-700">
          <thead>
            <tr className="bg-gray-200 dark:bg-gray-800">
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Nome
              </th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Descrição
              </th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Preço
              </th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Ações
              </th>
            </tr>
          </thead>
          <tbody>
            {products.map((product) => (
              <tr key={product.id} className="text-center">
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {product.name}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {product.description}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {product.price}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  <button className="bg-blue-500 dark:bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-600 dark:hover:bg-blue-800">
                    <Link href={`/purchase/${product.id}`}>Comprar</Link>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
