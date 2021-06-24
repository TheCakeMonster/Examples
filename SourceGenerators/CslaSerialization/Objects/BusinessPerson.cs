using Csla;
using CslaSerialization.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CslaSerialization.Objects
{

    [AutoSerializable]
    public partial class BusinessPerson : BusinessBase<BusinessPerson>
	{

		public static readonly Csla.PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(nameof(FirstName));
		public static readonly Csla.PropertyInfo<string> LastNameProperty = RegisterProperty<string>(nameof(LastName));

        #region Properties 

        [MaxLength(25)]
        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            set { SetProperty(FirstNameProperty, value); }
        }

        [Required]
        [MaxLength(25)]
        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            set { SetProperty(LastNameProperty, value); }
        }

        #endregion

        #region Factory Methods

        public static BusinessPerson NewBusinessPerson()
        {
            return DataPortal.Create<BusinessPerson>();
        }

        #endregion

        #region Data Access

        [RunLocal]
        [Create]
        private void Create()
        {
            // Trigger object checks
            BusinessRules.CheckRules();
        }

        #endregion

    }
}
