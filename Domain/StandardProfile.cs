﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserAPI.Domain
{
    public class StandardProfile : BaseProfile
    {
        #region PROPERTIES

        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }


        #endregion

        #region CONSTRUCTORS

        public StandardProfile(User user, string mail, string firstn, string lastn, string pic, DateTime birth, int sex) : base(user, mail, firstn, lastn, pic)
        {
            this.DateOfBirth = birth;
            this.Sex = sex;
        }

        public StandardProfile() : base()
        {
            this.DateOfBirth = DateTime.Now;
            this.Sex = 1;
        }

        #endregion
    }
}
