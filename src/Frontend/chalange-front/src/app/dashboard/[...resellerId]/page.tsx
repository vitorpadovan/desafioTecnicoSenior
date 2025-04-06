import ResellerInformation from "@/components/Reseller/ResellerInformation";
import ResellerProducts from "@/components/Reseller/ResellerProducts";

export default async function ResellerPage({
  params,
}: {
  readonly params: Promise<{ readonly resellerId: string }>;
}) {
  const resolvedParams = await params;

  return (
    <div className="flex justify-center mt-8">
      <div className="w-4/5">
        <ResellerInformation resellerId={resolvedParams.resellerId} />
        <ResellerProducts resellerId={resolvedParams.resellerId} />
      </div>
    </div>
  );
}