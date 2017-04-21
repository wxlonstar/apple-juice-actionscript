﻿using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    public class ClassMemberFinder
    {
        public static ClassMember find(
            Class cls,string name,
            Class finder
            
            )
        {
            for (int i = cls.classMembers.Count-1; i >=0; i--)
            {
                if (cls.classMembers[i].name == name)
                {
                    var member = cls.classMembers[i];
                    if (!member.isPublic)
                    {
                        if (finder != null)
                        {
                            if (member.isInternal)
                            {
                                if (finder.package == cls.package)
                                {
                                    return cls.classMembers[i];
                                }
                            }
                            else if (member.isPrivate)
                            {
                                if (finder == cls)
                                {
                                    return cls.classMembers[i];
                                }
                            }
                            else if (member.isProtectd)
                            {
                                if (isInherits(finder, cls))
                                {
                                    return cls.classMembers[i];
                                }
                            }

                            if (finder == cls)
                            {
                                return cls.classMembers[i];
                            }
                        }
                    }
                    else
                    {
                        return cls.classMembers[i];
                    }
                }
            }

            return null;
        }


        public static bool check_isinherits(IRunTimeValue value,RunTimeDataType type,IClassFinder classfinder)
        {
            var cls = classfinder.getClassByRunTimeDataType(type);
            return isInherits(((rtData.rtObject)value).value._class, cls);
        }

        public static bool check_isinherits(RunTimeDataType srcType, RunTimeDataType type, IClassFinder classfinder)
        {
            var srcCls = classfinder.getClassByRunTimeDataType(srcType);
            var cls = classfinder.getClassByRunTimeDataType(type);
            return isInherits(srcCls, cls);
        }



        public static bool isInherits(Class extendsClass,Class super)
        {
            var t = extendsClass;

            while (t !=null)
            {
                if (t == super)
                {
                    return true;
                }

                t = t.super;
            }

            return false;
        }


    }
}
