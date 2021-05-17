namespace LocaLabs.Domain.Entities.Base
{
    public class EntityValidation
    {
        public readonly string Field;
        public readonly string Reason;

        public EntityValidation(string reason, string field) =>
            (Field, Reason) = (reason, field);

        public override string ToString() =>
            $"{Field}: {Reason}";
    }
}