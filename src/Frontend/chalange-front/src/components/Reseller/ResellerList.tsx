//TODO esse componente pode
import { useEffect, useState } from "react";
import { ResellerService } from "../../api-services/reseller-services";
import { Reseller } from "../../interfaces/Reseller";
import Link from "next/link";

export default function ResellerList() {
  const [resellers, setResellers] = useState<Reseller[]>([]);

  useEffect(() => {
    ResellerService.getResellers()
      .then((data) => {
        setResellers(data);
      })
      .catch((error) => {
        console.error("Erro ao buscar revendedores:", error);
      });
  }, []);

  return (
    <div className="flex justify-center mt-8">
      <div className="w-4/5">
        <h1 className="text-center text-2xl font-bold mb-4">
          Lista de Revendedores
        </h1>
        <table className="table-auto w-full border-collapse border border-gray-300 dark:border-gray-700">
          <thead>
            <tr className="bg-gray-200 dark:bg-gray-800">
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Nome Fantasia
              </th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Razão Social
              </th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Documento
              </th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Email
              </th>
              <th className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                Ações
              </th>
            </tr>
          </thead>
          <tbody>
            {resellers.map((reseller) => (
              <tr key={reseller.id} className="text-center">
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {reseller.tradeName}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {reseller.registredName}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {reseller.document}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  {reseller.email}
                </td>
                <td className="border border-gray-300 dark:border-gray-700 px-4 py-2">
                  <Link href={`/dashboard/${reseller.id}`}>
                    <button 
                      className="bg-blue-500 dark:bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-600 dark:hover:bg-blue-800"
                    >
                      Comprar
                    </button>
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
