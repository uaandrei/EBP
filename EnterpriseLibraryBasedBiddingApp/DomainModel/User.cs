//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;



namespace DomainModel
{
    public partial class User
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual string Name
        {
            get;
            set;
        }
    
        public virtual string Password
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> BanEndDate
        {
            get;
            set;
        }
    
        public virtual int RoleId
        {
            get { return _roleId; }
            set
            {
                if (_roleId != value)
                {
                    if (Role != null && Role.Id != value)
                    {
                        Role = null;
                    }
                    _roleId = value;
                }
            }
        }
        private int _roleId;

        #endregion

        #region Navigation Properties
    
        public virtual ICollection<Bid> Bids
        {
            get
            {
                if (_bids == null)
                {
                    var newCollection = new FixupCollection<Bid>();
                    newCollection.CollectionChanged += FixupBids;
                    _bids = newCollection;
                }
                return _bids;
            }
            set
            {
                if (!ReferenceEquals(_bids, value))
                {
                    var previousValue = _bids as FixupCollection<Bid>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupBids;
                    }
                    _bids = value;
                    var newValue = value as FixupCollection<Bid>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupBids;
                    }
                }
            }
        }
        private ICollection<Bid> _bids;
    
        public virtual ICollection<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    var newCollection = new FixupCollection<Product>();
                    newCollection.CollectionChanged += FixupProducts;
                    _products = newCollection;
                }
                return _products;
            }
            set
            {
                if (!ReferenceEquals(_products, value))
                {
                    var previousValue = _products as FixupCollection<Product>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupProducts;
                    }
                    _products = value;
                    var newValue = value as FixupCollection<Product>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupProducts;
                    }
                }
            }
        }
        private ICollection<Product> _products;
    
        public virtual ICollection<UserRating> UserRatings
        {
            get
            {
                if (_userRatings == null)
                {
                    var newCollection = new FixupCollection<UserRating>();
                    newCollection.CollectionChanged += FixupUserRatings;
                    _userRatings = newCollection;
                }
                return _userRatings;
            }
            set
            {
                if (!ReferenceEquals(_userRatings, value))
                {
                    var previousValue = _userRatings as FixupCollection<UserRating>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUserRatings;
                    }
                    _userRatings = value;
                    var newValue = value as FixupCollection<UserRating>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUserRatings;
                    }
                }
            }
        }
        private ICollection<UserRating> _userRatings;
    
        public virtual Role Role
        {
            get { return _role; }
            set
            {
                if (!ReferenceEquals(_role, value))
                {
                    var previousValue = _role;
                    _role = value;
                    FixupRole(previousValue);
                }
            }
        }
        private Role _role;

        #endregion

        #region Association Fixup
    
        private void FixupRole(Role previousValue)
        {
            if (previousValue != null && previousValue.Users.Contains(this))
            {
                previousValue.Users.Remove(this);
            }
    
            if (Role != null)
            {
                if (!Role.Users.Contains(this))
                {
                    Role.Users.Add(this);
                }
                if (RoleId != Role.Id)
                {
                    RoleId = Role.Id;
                }
            }
        }
    
        private void FixupBids(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Bid item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Bid item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupProducts(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Product item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Product item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupUserRatings(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (UserRating item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (UserRating item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }

        #endregion

    }
}
