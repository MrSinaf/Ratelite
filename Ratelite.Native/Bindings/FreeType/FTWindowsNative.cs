using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    internal static unsafe partial class FTWindowsNative
    {
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Init_FreeType(FT_LibraryRec_** alibrary);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Done_FreeType(FT_LibraryRec_* library);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_New_Face(FT_LibraryRec_* library, byte* filepathname, CLong face_index, FT_FaceRec_** aface);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_New_Memory_Face(FT_LibraryRec_* library, byte* file_base, CLong file_size, CLong face_index, FT_FaceRec_** aface);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Open_Face(FT_LibraryRec_* library, FT_Open_Args_* args, CLong face_index, FT_FaceRec_** aface);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Attach_File(FT_FaceRec_* face, byte* filepathname);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Attach_Stream(FT_FaceRec_* face, FT_Open_Args_* parameters);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Reference_Face(FT_FaceRec_* face);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Done_Face(FT_FaceRec_* face);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Select_Size(FT_FaceRec_* face, int strike_index);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Request_Size(FT_FaceRec_* face, FT_Size_RequestRec_* req);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Set_Char_Size(FT_FaceRec_* face, CLong char_width, CLong char_height, uint horz_resolution, uint vert_resolution);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Set_Pixel_Sizes(FT_FaceRec_* face, uint pixel_width, uint pixel_height);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Load_Glyph(FT_FaceRec_* face, uint glyph_index, int load_flags);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Load_Char(FT_FaceRec_* face, CULong char_code, int load_flags);
        [LibraryImport("freetype.dll")]
        public static partial void FT_Set_Transform(FT_FaceRec_* face, FT_Matrix_* matrix, FT_Vector_* delta);
        [LibraryImport("freetype.dll")]
        public static partial void FT_Get_Transform(FT_FaceRec_* face, FT_Matrix_* matrix, FT_Vector_* delta);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Render_Glyph(FT_GlyphSlotRec_* slot, FT_Render_Mode_ render_mode);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Get_Kerning(FT_FaceRec_* face, uint left_glyph, uint right_glyph, uint kern_mode, FT_Vector_* akerning);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Get_Track_Kerning(FT_FaceRec_* face, CLong point_size, int degree, CLong* akerning);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Select_Charmap(FT_FaceRec_* face, FT_Encoding_ encoding);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Set_Charmap(FT_FaceRec_* face, FT_CharMapRec_* charmap);
        [LibraryImport("freetype.dll")]
        public static partial int FT_Get_Charmap_Index(FT_CharMapRec_* charmap);
        [LibraryImport("freetype.dll")]
        public static partial uint FT_Get_Char_Index(FT_FaceRec_* face, CULong charcode);
        [LibraryImport("freetype.dll")]
        public static partial CULong FT_Get_First_Char(FT_FaceRec_* face, uint* agindex);
        [LibraryImport("freetype.dll")]
        public static partial CULong FT_Get_Next_Char(FT_FaceRec_* face, CULong char_code, uint* agindex);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Face_Properties(FT_FaceRec_* face, uint num_properties, FT_Parameter_* properties);
        [LibraryImport("freetype.dll")]
        public static partial uint FT_Get_Name_Index(FT_FaceRec_* face, byte* glyph_name);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Get_Glyph_Name(FT_FaceRec_* face, uint glyph_index, void* buffer, uint buffer_max);
        [LibraryImport("freetype.dll")]
        public static partial byte* FT_Get_Postscript_Name(FT_FaceRec_* face);
        [LibraryImport("freetype.dll")]
        public static partial FT_Err FT_Get_SubGlyph_Info(FT_GlyphSlotRec_* glyph, uint sub_index, int* p_index, uint* p_flags, int* p_arg1, int* p_arg2, FT_Matrix_* p_transform);
        [LibraryImport("freetype.dll")]
        public static partial ushort FT_Get_FSType_Flags(FT_FaceRec_* face);
        [LibraryImport("freetype.dll")]
        public static partial uint FT_Face_GetCharVariantIndex(FT_FaceRec_* face, CULong charcode, CULong variantSelector);
        [LibraryImport("freetype.dll")]
        public static partial int FT_Face_GetCharVariantIsDefault(FT_FaceRec_* face, CULong charcode, CULong variantSelector);
        [LibraryImport("freetype.dll")]
        public static partial uint* FT_Face_GetVariantSelectors(FT_FaceRec_* face);
        [LibraryImport("freetype.dll")]
        public static partial uint* FT_Face_GetVariantsOfChar(FT_FaceRec_* face, CULong charcode);
        [LibraryImport("freetype.dll")]
        public static partial uint* FT_Face_GetCharsOfVariant(FT_FaceRec_* face, CULong variantSelector);
        [LibraryImport("freetype.dll")]
        public static partial CLong FT_MulDiv(CLong a, CLong b, CLong c);
        [LibraryImport("freetype.dll")]
        public static partial CLong FT_MulFix(CLong a, CLong b);
        [LibraryImport("freetype.dll")]
        public static partial CLong FT_DivFix(CLong a, CLong b);
        [LibraryImport("freetype.dll")]
        public static partial CLong FT_RoundFix(CLong a);
        [LibraryImport("freetype.dll")]
        public static partial CLong FT_CeilFix(CLong a);
        [LibraryImport("freetype.dll")]
        public static partial CLong FT_FloorFix(CLong a);
        [LibraryImport("freetype.dll")]
        public static partial void FT_Vector_Transform(FT_Vector_* vector, FT_Matrix_* matrix);
        [LibraryImport("freetype.dll")]
        public static partial void FT_Library_Version(FT_LibraryRec_* library, int* amajor, int* aminor, int* apatch);
        [LibraryImport("freetype.dll")]
        public static partial byte FT_Face_CheckTrueTypePatents(FT_FaceRec_* face);
        
        [LibraryImport("freetype.dll")]
        public static partial byte FT_Face_SetUnpatentedHinting(FT_FaceRec_* face, byte value);
    }
}