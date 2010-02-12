using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class BooleanExt
    {
        public static Boolean Ext_Not(this Boolean iBool)
        {
            return !iBool;
        }
        public static void Ext_Do(this  Boolean iBool, Action actionIfTrue)
        {
            if (iBool)
            {
                actionIfTrue();
            }
        }
        public static void Ext_Do(this  Boolean iBool, Action actionIfTrue, Action actionIfFalse)
        {
            if (iBool)
            {
                actionIfTrue();
            }
            else
            {
                actionIfFalse();
            }
        }

        /// <summary>
        /// Disjunction
        /// </summary>
        /// <param name="iBool"></param>
        /// <param name="iBool2"></param>
        /// <returns></returns>
        public static bool Ext_NAND(this Boolean iBool, Boolean iBool2)
        {
            if(iBool == true && iBool2 == true)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// BI-Negation ( joint denial )
        /// </summary>
        /// <param name="iBool"></param>
        /// <param name="iBool2"></param>
        /// <returns></returns>
        public static bool Ext_NOR(this Boolean iBool, Boolean iBool2)
        {
            if(iBool == false && iBool2== false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Logical implication
        /// </summary>
        /// <param name="iBool"></param>
        /// <param name="iBool2"></param>
        /// <returns></returns>
        public static bool Ext_IMP(this Boolean iBool, Boolean iBool2)
        {
            if(iBool == true && iBool2== false)
            {
                return false;
            }
            return true;
            
        }
        public static bool Ext_AND(this Boolean iBool, Boolean iBool2)
        {
            if(iBool==true &&  iBool2== true)
            {
                return true;
            }
            return false;
        }
        public static bool Ext_OR(this Boolean iBool, Boolean iBool2)
        {
            if(iBool ==true || iBool2== true)
            {
                return true;
            }
            return false;
        }
        
        public static bool Ext_XOR(this Boolean iBool, Boolean iBool2)
        {
            if((iBool== true && iBool2==false ) ||(iBool== false && iBool2==true))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Logical biconditional
        /// </summary>
        /// <param name="iBool"></param>
        /// <param name="iBool2"></param>
        /// <returns></returns>
        public static bool Ext_EQ(this Boolean iBool, Boolean iBool2)
        {
            if(iBool == iBool2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Converse nonimplication
        /// not A implies B"
        /// </summary>
        /// <param name="iBool"></param>
        /// <param name="iBool2"></param>
        /// <returns></returns>
        public static bool Ext_NonIMP_Converse(this Boolean iBool, Boolean iBool2)
        {
            if (iBool ==false &&  iBool2== true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Material nonimplication 
        /// "P and not Q" 
        /// </summary>
        /// <param name="iBool"></param>
        /// <param name="iBool2"></param>
        /// <returns></returns>
        public static bool Ext_NonIMP_Material(this Boolean iBool, Boolean iBool2)
        {
            if (iBool ==true &&  iBool2== false)
            {
                return true;
            }
            return false;
        }


        public static int Ext_ToInt(this Boolean iBool)
        {
            if (iBool)
                return 1;
            return 0;
        }
    }
}
