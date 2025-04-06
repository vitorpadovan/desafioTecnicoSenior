'use client';
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
      .catch((error) => {
        
      });
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
        <p>
          <strong>Razão Social:</strong> {resellerData.registredName}
        </p>
        <p>
          <strong>Nome Fantasia:</strong> {resellerData.tradeName}
        </p>
        <p>
          <strong>Email:</strong> {resellerData.email}
        </p>
      </div>
    </div>
  );
}
