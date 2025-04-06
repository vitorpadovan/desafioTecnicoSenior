"use client";
import { ResellerService } from "@/api-services/reseller-services";
import { ResellerInformationData } from "@/interfaces/Reseller";
import { useEffect, useState } from "react";

export default function ResellerInformation(params: {
  readonly resellerId: string;
}) {
  const [resellerData, setResellerData] =
    useState<ResellerInformationData | null>(null);
  useEffect(() => {
    ResellerService.getResellerInformation(params.resellerId)
      .then((data) => {
        setResellerData(data);
      })
      .catch((error) => {});
  }, [params.resellerId]);

  if (!resellerData) {
    return (
      <p className="text-center mt-8">
        Carregando informações do revendedor...
      </p>
    );
  }

  return (
    <div className="flex justify-center mt-8">
      <div className="w-4/5">
        <h1 className="text-center text-2xl font-bold mb-4">
          Informações do Revendedor
        </h1>
        <table className="table-auto w-full">
          <thead>
            <tr>
              <th className="px-4 py-2 text-center w-1/3">Razão Social</th>
              <th className="px-4 py-2 text-center w-1/3">Nome Fantasia</th>
              <th className="px-4 py-2 text-center w-1/3">Email</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td className="px-4 py-2 text-center w-1/3">{resellerData.registredName}</td>
              <td className="px-4 py-2 text-center w-1/3">{resellerData.tradeName}</td>
              <td className="px-4 py-2 text-center w-1/3">{resellerData.email}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
}
