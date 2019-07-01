using System;
using System.ComponentModel;

namespace TauCode.WebApi
{
    [TypeConverter(typeof(IdDtoTypeConverter))]
    [Serializable]
    public sealed class IdDto : IEquatable<IdDto>
    {
        #region Field

        private readonly Guid _id;

        #endregion

        #region Constructors

        public IdDto(Guid id)
        {
            _id = id;
        }

        public IdDto(string id)
        {
            _id = new Guid(id);
        }

        #endregion

        #region IEquatable<IdDto> Members

        public bool Equals(IdDto other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (object.ReferenceEquals(null, other))
            {
                return false;
            }

            return this._id.Equals(other._id);

        }

        #endregion

        #region Overridden

        public override bool Equals(object another)
        {
            return this.Equals(another as IdDto);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        #endregion

        #region Public

        public Guid GetId() => _id;

        #endregion

        #region Operators

        public static bool operator ==(IdDto id1, IdDto id2)
        {
            return Equals(id1, id2);
        }

        public static bool operator !=(IdDto id1, IdDto id2)
        {
            return !Equals(id1, id2);
        }

        #endregion
    }
}
