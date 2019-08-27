using Kiosko.Models;
using System;
using System.Globalization;
using System.IO;

using static Kiosko.Models.CubiQModel;

namespace Kiosko.Helpers
{
    /***
     *  This class handle all Customer | Kiosko Restrictions
     * 
     * */
    public class KioskoRestrictions
    {

        /**
         * Check if a given meaure is valid, it means 
         * Package is valid = Le && Wi && We && Vw > 0
         * Label is valid : !todo!!
         * */
        public static bool IsValidMeasure(Measure measure, string mode)
        {
            if (mode.ToLower().Equals("package"))
            {
                return measure.Height * measure.Length * measure.Weight * measure.Width * measure.VolumetricWeight > 0;
            }
            if (mode.ToLower().Equals("envelope"))
            {
                return true;
            }
            return false;
        }


        /**
         * Check if a given measure is higher than the Customer Max Measure for package or envelope(CubiQ Manager)
         * 
         * */
        public static bool IsMaxMeasure(Measure measure, string mode)
        {            
            var kioskoConfiguration = Helpers.Utilities.GetKioskoConfiguration();
            var configuration = kioskoConfiguration.configuration;

            if (mode.Equals("package"))
            {
                return (
                     (measure.Height > Convert.ToDouble(configuration.max_package_height_value, CultureInfo.InvariantCulture)) ||
                     (measure.Weight > Convert.ToDouble(configuration.max_package_weight_value, CultureInfo.InvariantCulture)) ||
                     (measure.Width > Convert.ToDouble(configuration.max_package_width_value, CultureInfo.InvariantCulture)) ||
                     (measure.Length > Convert.ToDouble(configuration.max_package_length_value, CultureInfo.InvariantCulture))
                   );
            }

            if (mode.Equals("envelope"))
            {
                return (
                     (Convert.ToDouble(measure.Weight, CultureInfo.InvariantCulture) > Convert.ToDouble(configuration.max_envelope_weight_value, CultureInfo.InvariantCulture)) ||
                     (Convert.ToDouble(measure.Width, CultureInfo.InvariantCulture) > Convert.ToDouble(configuration.max_envelope_width_value, CultureInfo.InvariantCulture)) ||
                     (Convert.ToDouble(measure.Length, CultureInfo.InvariantCulture) > Convert.ToDouble(configuration.max_envelope_length_value, CultureInfo.InvariantCulture))
                   );
            }

            return false;


        }
        
        /**
         * Check if a given measure if SMALLER than the Customer MIN Measure for package or envelope (CubiQ Manager)
         * TODO:Do it for label 
         * */
        public static bool IsMinMeasure(Measure measure, string mode)
        {
            var kioskoConfiguration = Helpers.Utilities.GetKioskoConfiguration();
            var configuration = kioskoConfiguration.configuration;
            return (measure.Weight < Convert.ToDouble(configuration.min_package_weight_value, CultureInfo.InvariantCulture));
        }

        /**
         * Check if a given value is higher than the Customer Max Declared Value (CubiQ Manager)
         * */
        public static bool IsMaxDeclaredValue(double value)
        {
            var kioskoConfiguration = Helpers.Utilities.GetKioskoConfiguration();
            var configuration = kioskoConfiguration.configuration;
            return (value >  Convert.ToDouble(configuration.max_declared_value, CultureInfo.InvariantCulture));
        }

    }
}