namespace Deblue.ObservingSystem
{
    [System.Serializable]
    public class ObservBool : ObservProperty<bool>
    {
        public ObservBool(bool value) : base(value)
        {
        }
        
        public ObservBool() : base(false)
        {
        }

        public static bool operator ==(ObservBool a, bool b) => (a.Value == b);
        public static bool operator !=(ObservBool a, bool b) => (a.Value != b);

        public static bool operator ==(ObservBool a, ObservBool b) => (a.Value == b.Value);
        public static bool operator !=(ObservBool a, ObservBool b) => (a.Value != b.Value);

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hashCode = _value.GetHashCode();
            return hashCode;
        }
    }
}
