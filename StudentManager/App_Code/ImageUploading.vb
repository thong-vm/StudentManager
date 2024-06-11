Imports Microsoft.SqlServer
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class ImageUploading
    Inherits System.Web.UI.Page
    Function GetUrl(ByVal fileAvatar As FileUpload
                             )
        If fileAvatar.HasFile Then
            Try
                'If fileAvatar.PostedFile.ContentLength > 512000 Then ' 500kb = 512000 bytes
                '    Throw New Exception("File size must be less than 500kb.")
                'End If

                Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif"}
                Dim fileName As String = Path.GetFileName(fileAvatar.PostedFile.FileName)
                Dim fileExtension As String = Path.GetExtension(fileName)

                'If Not allowedExtensions.Contains(fileExtension) Then
                '    Throw New Exception("Only image files (.jpg, .jpeg, .png, .gif) are allowed.")
                'End If

                Dim newFileName As String = Guid.NewGuid().ToString() & fileExtension

                Dim uploadPath As String = Server.MapPath("~/Uploads/Avatars/")
                If Not Directory.Exists(uploadPath) Then
                    Directory.CreateDirectory(uploadPath)
                End If


                Dim tempPath As String = Path.Combine(uploadPath, "temp_" & newFileName)
                fileAvatar.PostedFile.SaveAs(tempPath)

                Dim compressedPath As String = Path.Combine(uploadPath, newFileName)
                CompressImage(tempPath, compressedPath)

                If File.Exists(tempPath) Then
                    File.Delete(tempPath)
                End If

                Return "/Uploads/Avatars/" & newFileName
            Catch ex As Exception
                Return ""
            End Try
        End If
        Return ""
    End Function

    Private Sub CompressImage(ByVal sourcePath As String, ByVal destPath As String)
        Dim quality As Long = 100
        Dim maxSize As Long = 512000

        Do
            Using image As Image = Image.FromFile(sourcePath)
                Dim width As Integer = image.Width
                Dim height As Integer = image.Height
                Dim resizedImage As New Bitmap(width, height)

                Using graphics As Graphics = Graphics.FromImage(resizedImage)
                    graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                    graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                    graphics.DrawImage(image, New Rectangle(0, 0, width, height))
                End Using

                Dim codec As ImageCodecInfo = GetEncoderInfo("image/jpeg")
                Dim encoder As Encoder = Encoder.Quality
                Dim encoderParams As New EncoderParameters(1)
                encoderParams.Param(0) = New EncoderParameter(encoder, quality)

                resizedImage.Save(destPath, codec, encoderParams)
            End Using

            Dim fileInfo As New FileInfo(destPath)
            If fileInfo.Length <= maxSize Then
                Exit Do
            Else
                quality -= 1
                If quality < 0 Then
                    Throw New Exception("Unable to compress image below 500KB.")
                End If
            End If
        Loop
    End Sub

    Private Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()
        For Each codec As ImageCodecInfo In codecs
            If codec.MimeType = mimeType Then
                Return codec
            End If
        Next
        Return Nothing
    End Function
End Class
