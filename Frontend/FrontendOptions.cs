using Models;

namespace Frontend;

public record FrontendOptions(
    string ApiPrefix,

    string AddressRoot
) : SharedOptions(ApiPrefix);
