using Microsoft.AspNetCore.Mvc;


namespace Api.Attributes.Authorization.Outbox;

public class OutboxAuthAttribute : TypeFilterAttribute
{
    public OutboxAuthAttribute() : base(typeof(OutboxAuthAction))
    {
    }
}
