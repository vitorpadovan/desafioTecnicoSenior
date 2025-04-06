import ResellerInformation from "@/components/Reseller/ResellerInformation";
import ResellerProducts from "@/components/Reseller/ResellerProducts";

export default function ResellerPage({
  params,
}: {
  readonly params: { readonly resellerId: string };
}) {
  return (
    <div className="flex justify-center mt-8">
      <div className="w-4/5">
        <ResellerInformation resellerId={params.resellerId} />
        <ResellerProducts resellerId={params.resellerId} />
      </div>
    </div>
  );
}