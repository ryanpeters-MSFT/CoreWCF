using ServiceReference1;

using (var client = new ServiceClient())
{
    var compositeType = new CompositeType
    {
        BoolValue = true,
        StringValue = "Hello"
    };

    var response = await client.GetDataUsingDataContractAsync(compositeType);

    Console.WriteLine(response.StringValue);
}