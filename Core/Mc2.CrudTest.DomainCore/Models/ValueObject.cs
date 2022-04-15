namespace Mc2.CrudTest.DomainCore.Models
{
    public abstract class ValueObject<T> where T: ValueObject<T>
    {
        protected abstract bool EqualsCore(T? other);
        protected abstract int GetHashCodeCore();

        public override bool Equals(object? obj)
        {
            T? valueObject = obj as T;
            return EqualsCore(valueObject);
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        public static bool operator ==(ValueObject<T>? a, ValueObject<T>? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

    }
}